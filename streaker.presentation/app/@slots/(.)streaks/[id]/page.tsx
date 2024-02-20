"use client";
import { useParams, useRouter } from "next/navigation";
import Modal from "../../../components/Modal";
import StreakComponent from "@/app/components/StreakComponent";

export default function Streak() {
	const router = useRouter();
	const params = useParams();
	return (
		<Modal close={() => router.back()} large>
			<StreakComponent id={params.id as string} />
		</Modal>
	);
}
