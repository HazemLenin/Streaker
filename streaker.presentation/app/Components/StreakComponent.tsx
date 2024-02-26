import { useEffect, useState } from "react";
import useAxios from "../hooks/useAxios";
import { useRouter } from "next/navigation";
import NotFound from "../not-found";
import Calendar from "./Calendar";

export default function StreakComponent({ id }: { id: string }) {
	const axios = useAxios();
	const [loading, setLoading] = useState(true);
	const [streak, setStreak] = useState<any>();
	const router = useRouter();

	useEffect(() => {
		axios
			.get(`/api/streaks/${id}`)
			.then((res) => {
				setStreak(res.data.data);
				setLoading(false);
			})
			.catch((err) => {
				setLoading(false);
			});
	}, []);
	return (
		<>
			{loading ? (
				<div className="skeleton w-full h-20"></div>
			) : streak ? (
				<div className="flex flex-col gap-10 items-center">
					<h1 className="text-4xl font-bold mb-5">{streak.name}</h1>
					<Calendar className="w-full" streakId={streak.id} />
				</div>
			) : (
				<NotFound />
			)}
		</>
	);
}
