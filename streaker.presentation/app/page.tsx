"use client";
import { useSelector } from "react-redux";
import { IRootState } from "./store";
import { Inter, Pacifico } from "next/font/google";

const pacifico = Pacifico({ weight: ["400"], subsets: ["latin"] });

export default function Home() {
	const tokens = useSelector((state: IRootState) => state.tokens);

	return (
		<div className="pt-24 px-2 md:px-20">
			<section className="md:flex justify-between">
				<div className="flex flex-col items-center md:block md:w-2/3">
					<h1
						className={`text-5xl md:text-7xl font-bold mb-5 ${pacifico.className}`}
					>
						Streaker
					</h1>
					<p>Keeps your streaks up!</p>
				</div>
				<div className="hidden md:block w-1/3">
					<img src="/svg/Jogging-cuate.svg" />
				</div>
			</section>
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
								<h1 className="text-4xl">Show modal</h1>
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
