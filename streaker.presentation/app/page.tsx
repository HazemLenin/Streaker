"use client";
import Modal from "./Components/Modal";
import { useState } from "react";
import { motion, AnimatePresence } from "framer-motion";
import Calendar from "./Components/Calendar";

export default function Home() {
	const [showModal, setShowModal] = useState(false);

	return (
		<div className="flex justify-center items-center h-screen">
			<Calendar />
			{/* <button className="btn btn-primary" onClick={() => setShowModal(true)}>
				Show Modal
			</button>
			<AnimatePresence mode="wait">
				{showModal && (
					<motion.div
						className="fixed inset-0"
						key="modal"
						initial={{ opacity: 0 }}
						animate={{ opacity: 1 }}
						exit={{ opacity: 0 }}
					>
						<Modal close={() => setShowModal(false)} large>
							<div className="flex flex-col items-center">
								<h1 className="text-4xl font-bold">Show modal</h1>
								<button
									className="btn btn-secondary"
									onClick={() => setShowModal(false)}
								>
									Dimiss
								</button>
							</div>
						</Modal>
					</motion.div>
				)}
			</AnimatePresence> */}
		</div>
	);
}
