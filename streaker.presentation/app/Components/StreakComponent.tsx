import { useEffect, useState } from "react";
import useAxios from "../hooks/useAxios";
import { useRouter } from "next/navigation";
import NotFound from "../not-found";

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
				if (err.response.status === 404) router;
				setLoading(false);
			});
	}, []);
	return (
		<>
			{loading ? (
				<div className="skeleton w-full h-20"></div>
			) : streak ? (
				<div>{streak.name}</div>
			) : (
				<NotFound />
			)}
		</>
	);
}
