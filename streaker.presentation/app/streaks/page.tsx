"use client";
import { useState } from "react";

export default function Streaks() {
	const [loading, setLoading] = useState(true);

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
					<div className="flex flex-col gap-5"></div>
				</>
			)}
		</>
	);
}
