import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import Navbar from "./components/Navbar";
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer } from "react-toastify";
import AppProvider from "./components/AppProvider";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
	title: "Streaker",
	description: "Keeps up your streaks!",
};

export default function RootLayout({
	children,
}: {
	children: React.ReactNode;
}) {
	return (
		<AppProvider>
			<html lang="en">
				<body className={inter.className}>
					<Navbar />
					<main className="px-2 md:px-10">{children}</main>
					<ToastContainer />
				</body>
			</html>
		</AppProvider>
	);
}
