// components/Calendar.js
import React, { useState } from "react";
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
} from "@fortawesome/free-solid-svg-icons";

const Calendar = () => {
	const today = new Date();
	const [selectedDate, setSelectedDate] = useState(new Date());

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

	return (
		<div>
			<div className="flex items-center justify-between mb-4">
				<button className="calendar-btn" onClick={prevMonth}>
					<FontAwesomeIcon icon={faChevronLeft} />
				</button>
				<h2 className="text-2xl">{format(selectedDate, "MMMM yyyy")}</h2>
				<button className="calendar-btn" onClick={nextMonth}>
					<FontAwesomeIcon icon={faChevronRight} />
				</button>
			</div>
			<div className="grid grid-cols-7 gap-4">
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
						className={`cursor-default calendar-btn ${
							isSameDay(day, today) && "today"
						}`}
						onClick={() => handleDateClick(day)}
					>
						{format(day, "d")}
					</div>
				))}
			</div>
		</div>
	);
};

export default Calendar;
