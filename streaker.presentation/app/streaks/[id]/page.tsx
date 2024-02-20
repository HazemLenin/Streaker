"use client";
import StreakComponent from "@/app/components/StreakComponent";
import { useParams } from "next/navigation";

export default function Streak() {
	const params = useParams();
	return (
		<div className="pt-24 px-2 md:px-20">
			<StreakComponent id={params.id as string} />
		</div>
	);
}
