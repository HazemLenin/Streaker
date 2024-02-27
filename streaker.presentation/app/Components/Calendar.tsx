// components/Calendar.js
import React, { MouseEventHandler, useEffect, useState } from "react";
import {
	format,
	addMonths,
	subMonths,
	startOfMonth,
	endOfMonth,
	eachDayOfInterval,
	isSameMonth,
	startOfWeek,
	isSameDay,
} from "date-fns";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
	faChevronLeft,
	faChevronRight,
	faCircleNotch,
} from "@fortawesome/free-solid-svg-icons";
import useAxios from "../hooks/useAxios";
import { toast } from "react-toastify";
import { useRouter } from "next/navigation";
import { useWindowSize } from "react-use";
import Confetti from "react-confetti";

const Calendar = ({
	className,
	streakId,
}: {
	className?: string;
	streakId: string;
}) => {
	const today = new Date();
	const [selectedDate, setSelectedDate] = useState(new Date());
	const axios = useAxios();
	const [loading, setLoading] = useState(true);
	const [data, setData] = useState<Array<number>>([]);
	const [commitedToday, setCommitedToday] = useState(false);
	const [commitLoading, setCommitLoading] = useState(false);
	const router = useRouter();
	const { width, height } = useWindowSize();
	const [celebrate, setCelebrate] = useState(false);

	const handleDateClick = (day: Date) => {
		setSelectedDate(day);
	};

	const nextMonth = () => {
		setSelectedDate(addMonths(selectedDate, 1));
	};

	const prevMonth = () => {
		setSelectedDate(subMonths(selectedDate, 1));
	};

	const daysInMonth = eachDayOfInterval({
		start: startOfMonth(selectedDate),
		end: endOfMonth(selectedDate),
	});

	const weekDays = ["Su", "Mo", "Tu", "We", "Th", "Fr", "Sa"];

	function getEmptyDays() {
		let currentWeekDay = 0;
		let firstDayInMonth = startOfMonth(selectedDate).getDay();
		let emptyDays = [];
		while (currentWeekDay != firstDayInMonth) {
			currentWeekDay++;
			emptyDays.push(0);
		}
		return emptyDays;
	}

	useEffect(() => {
		axios
			.get(`/api/streaks/getCurrentMonthStreak/${streakId}`)
			.then((res) => {
				setData(res.data.data.commits);
				setCommitedToday(res.data.data.commitedToday);
			})
			.catch((err) =>
				toast.error("Something went wrong!", { theme: "colored" })
			)
			.finally(() => setLoading(false));
	}, []);

	const handleCommitClick: MouseEventHandler<HTMLButtonElement> = (e) => {
		setCommitLoading(true);
		axios
			.post(`api/streaks/commitToday/${streakId}`)
			.then((res) => {
				toast.success("Congrats!!", { theme: "colored" });
				setCelebrate(true);
				setTimeout(() => {
					setCelebrate(false);
				}, 5000);
				router.push("/streaks");
			})
			.catch((err) =>
				toast.error("Something went wrong!", { theme: "colored" })
			)
			.finally(() => setCommitLoading(false));
	};

	return (
		<div className={className}>
			{celebrate && <Confetti />}
			{loading ? (
				<div className="skeleton w-full h-96"></div>
			) : (
				<div className="flex flex-col items-center gap-10">
					<div className="flex items-center justify-center">
						{/* <button className="calendar-btn" onClick={prevMonth}>
					<FontAwesomeIcon icon={faChevronLeft} />
				</button> */}
						<h2 className="text-2xl">{format(selectedDate, "MMMM yyyy")}</h2>
						{/* <button className="calendar-btn" onClick={nextMonth}>
					<FontAwesomeIcon icon={faChevronRight} />
				</button> */}
					</div>
					<div className="grid grid-cols-7 gap-4 w-full">
						{weekDays.map((day) => (
							<div key={day} className="text-center">
								{day}
							</div>
						))}
						{getEmptyDays().map((day, index) => (
							<div key={index}></div>
						))}
						{daysInMonth.map((day) => (
							<div
								key={day.toISOString()}
								className={`cursor-default calendar-btn mx-auto ${
									isSameDay(day, today) && "today"
								}`}
								onClick={() => handleDateClick(day)}
							>
								{format(day, "d")}
							</div>
						))}
					</div>
					{!commitedToday && (
						<button className="btn btn-primary" onClick={handleCommitClick}>
							{commitLoading ? (
								<FontAwesomeIcon icon={faCircleNotch} spin />
							) : (
								"Commit"
							)}
						</button>
					)}
				</div>
			)}
		</div>
	);
};

export default Calendar;
