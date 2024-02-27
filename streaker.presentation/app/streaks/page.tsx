"use client";
import { useEffect, useState } from "react";
import useAxios from "../hooks/useAxios";
import { toast } from "react-toastify";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
	faDrumstickBite,
	faMugHot,
	faRunning,
} from "@fortawesome/free-solid-svg-icons";
import Link from "next/link";
import truncateText from "../utils/truncateText";

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
				className="flex items-end p-5 md:px-20 h-[50vh] mb-5 relative
				bg-[url('/svg/Smart-home-rafiki.svg')]
				bg-no-repeat
				bg-cover
				bg-center
				
				before:content-['']
				before:absolute
				before:inset-0
				before:bg-gradient-to-t
				before:from-black/90
				before:to-black/10"
			>
				<h1 className="text-4xl md:text-7xl font-bold z-10">Streaks</h1>
			</div>
			{loading ? (
				<div className="flex flex-col gap-10 px-2 md:px-20">
					<div className="skeleton w-full h-20 md:h-40"></div>
					<div className="skeleton w-full h-20 md:h-40"></div>
					<div className="skeleton w-full h-20 md:h-40"></div>
					<div className="skeleton w-full h-20 md:h-40"></div>
					<div className="skeleton w-full h-20 md:h-40"></div>
				</div>
			) : (
				<>
					<div className="flex flex-col gap-5 px-5 md:px-20">
						{streaks.map((streak: any) => (
							<Link
								href={`/streaks/${streak.id}`}
								className="flex jutsify-between md:hover:bg-muted border border-muted p-5 rounded-lg transition-colors"
							>
								<div className="flex gap-5 md:gap-10 w-full">
									<div className="flex justify-center items-center text-5xl md:text-7xl md:p-5 h-20 md:h-40 w-20 md:w-40 bg-primary rounded-xl">
										{streak.category === "food" && (
											<FontAwesomeIcon icon={faDrumstickBite} />
										)}
										{streak.category === "drink" && (
											<FontAwesomeIcon icon={faMugHot} />
										)}
										{streak.category === "sports" && (
											<FontAwesomeIcon icon={faRunning} />
										)}
									</div>
									<div className="flex-shrink-[100]">
										<h2 className="text-2xl md:text-4xl font-bold">
											{streak.name}
										</h2>
										<p className="text-lg md:hidden">
											{streak.streakCount}/{streak.targetCount}
										</p>
										<p className="text-lg hidden md:block">
											{truncateText(
												"klasjdfl; kasjdflk;aj sdf;jaksld; fjakls; dfj; alskjdf; alsjdfla; djsfak;klasjdfl; kasjdflk;aj sdf;jaksld; fjakls; dfj; alskjdf; alsjdfla; djsfak;klasjdfl; kasjdflk;aj sdf;jaksld; fjakls; dfj; alskjdf; alsjdfla; djsfak;klasjdfl; kasjdflk;aj sdf;jaksld; fjakls; dfj; alskjdf; alsjdfla; djsfak;klasjdfl; kasjdflk;aj sdf;jaksld; fjakls; dfj; alskjdf; alsjdfla; djsfak;klasjdfl; kasjdflk;aj sdf;jaksld; fjakls; dfj; alskjdf; alsjdfla; djsfak;klasjdfl; kasjdflk;aj sdf;jaksld; fjakls; dfj; alskjdf; alsjdfla; djsfak;klasjdfl; kasjdflk;aj sdf;jaksld; fjakls; dfj; alskjdf; alsjdfla; djsfak;klasjdfl; kasjdflk;aj sdf;jaksld; fjakls; dfj; alskjdf; alsjdfla; djsfak;klasjdfl; kasjdflk;aj sdf;jaksld; fjakls; dfj; alskjdf; alsjdfla; djsfak;klasjdfl; kasjdflk;aj sdf;jaksld; fjakls; dfj; alskjdf; alsjdfla; djsfak;klasjdfl; kasjdflk;aj sdf;jaksld; fjakls; dfj; alskjdf; alsjdfla; djsfak;klasjdfl; kasjdflk;aj sdf;jaksld; fjakls; dfj; alskjdf; alsjdfla; djsfak;",
												20
											)}
										</p>
									</div>
								</div>
								<div className="hidden md:block">
									<h3 className="text-xl md:text-3xl font-bold">
										{streak.streakCount}/{streak.targetCount}
									</h3>
								</div>
							</Link>
						))}
					</div>
				</>
			)}
		</>
	);
}
