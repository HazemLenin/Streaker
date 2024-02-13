"use client";
import { useParams, useRouter } from "next/navigation";
import Modal from "../../../components/Modal";

export default function Streak() {
	const router = useRouter();
	const params = useParams();
	return (
		<Modal close={() => router.back()} large>
			<div className="flex flex-col items-center">
				<h1 className="text-4xl">Streak {params.id}</h1>
				<button className="btn btn-secondary" onClick={() => router.back()}>
					Dimiss
				</button>
			</div>
		</Modal>
	);
}
