"use client";
import { useRouter } from "next/navigation";

export default function Streak() {
	const router = useRouter();
	return <h1 className="text-4xl font-bold pt-24">Streak works!</h1>;
}
