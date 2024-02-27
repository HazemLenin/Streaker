"use client";
import { useRouter } from "next/navigation";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { remove_tokens } from "../actions";

export default function Logout() {
	const dispatch = useDispatch();
	const router = useRouter();
	useEffect(() => {
		dispatch(remove_tokens());
		router.push("/");
	}, []);
	return (
		<div className="pt-24">
			<h1 className="text-4xl font-bold text-center">Redirecting...</h1>
			<p className="text-center">You are being redirected to the home page.</p>
		</div>
	);
}
