"use client";
import { useEffect, useState } from "react";
import useAxios from "../hooks/useAxios";
import { toast } from "react-toastify";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faRunning } from "@fortawesome/free-solid-svg-icons";

export default function Streaks() {
	const [loading, setLoading] = useState(true);
	const axios = useAxios();
	const [streaks, setStreaks] = useState([]);

	useEffect(() => {
		axios
			.get("/api/streaks/")
			.then((res) => {
				setLoading(false);
				setStreaks(res.data.data);
			})
			.catch((err) => {
				toast.error("Somehting went wrong!", {
					theme: "colored",
				});
			});
	}, []);
	return (
		<>
			<div
				className="flex items-end p-5 md:px-20 h-[50vh] mb-10 relative
				bg-[url('/svg/Smart-home-rafiki.svg')]
				bg-no-repeat
				bg-cover
				bg-center
				
				before:content-['']
				before:absolute
				before:inset-0
				before:bg-gradient-to-t
				before:from-black
				before:to-black/10"
			>
				<h1 className="text-4xl md:text-7xl font-bold z-10">Streaks</h1>
			</div>
			{loading ? (
				<div className="flex flex-col gap-5 px-2 md:px-20">
					<div className="skeleton w-full h-20 md:h-32"></div>
					<div className="skeleton w-full h-20 md:h-32"></div>
					<div className="skeleton w-full h-20 md:h-32"></div>
					<div className="skeleton w-full h-20 md:h-32"></div>
					<div className="skeleton w-full h-20 md:h-32"></div>
				</div>
			) : (
				<>
					<div className="flex flex-col gap-5 px-2 md:px-20">
						{streaks.map((streak: any) => (
							<div className="flex jutsify-between p-2">
								<div className="flex gap-5 md:gap-10 w-full">
									<div className="flex justify-center items-center text-5xl md:text-7xl h-20 md:h-40 w-20 md:w-40 bg-primary rounded-lg">
										<FontAwesomeIcon icon={faRunning} />
									</div>
									<div>
										<h2 className="text-2xl md:text-4xl font-bold">
											{streak.name}
										</h2>
										<p className="text-muted text-lg">{streak.description}</p>
									</div>
								</div>
								<div className="hidden md:block">
									<h3 className="text-xl md:text-3xl font-bold">
										{streak.streakCount}/{streak.targetCount}
									</h3>
								</div>
							</div>
						))}
					</div>
				</>
			)}
		</>
	);
}
