"use client";
import { useRouter } from "next/navigation";
import Modal from "../../components/Modal";

export default function Streak() {
	const router = useRouter();
	return (
		<Modal close={() => router.back()} large>
			<div className="flex flex-col items-center">
				<h1 className="text-4xl">Show modal</h1>
				<button className="btn btn-secondary" onClick={() => router.back()}>
					Dimiss
				</button>
			</div>
		</Modal>
	);
}
