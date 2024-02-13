"use client";
import { useParams } from "next/navigation";

export default function Streak() {
	const params = useParams();
	return (
		<h1 className="text-4xl font-bold pt-24">Streak works! {params.id}</h1>
	);
}
