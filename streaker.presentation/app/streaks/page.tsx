"use client";
import { useState } from "react";

export default function Streaks() {
	const [loading, setLoading] = useState(true);

	return (
		<>
			<div className="flex items-end p-2 md:px-20 h-[50vh] bg-primary mb-10">
				<h1 className="text-4xl md:text-7xl font-bold">Streaks</h1>
			</div>
			{loading ? (
				<div className="flex flex-col gap-5">
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
