"use client";
import Link from "next/link";
import logo from "../../public/Streaker Logo.png";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
	faDoorOpen,
	faSignIn,
	faSignOut,
	faUserPlus,
} from "@fortawesome/free-solid-svg-icons";
import { useSelector } from "react-redux";
import { IRootState } from "../store";
import { useEffect, useState } from "react";
import { Pacifico } from "next/font/google";

const pacifico = Pacifico({ weight: ["400"], subsets: ["latin"] });

export default function Navbar() {
	const tokens = useSelector((state: IRootState) => state.tokens);

	return (
		<nav className="flex items-center justify-between px-2 md:px-20 h-16 border-b border-muted shadow-lg fixed inset-0 w-full backdrop-blur-sm">
			<ul className="flex items-center gap-10">
				<li>
					<Link href="/" className="flex gap-2 items-center">
						<img src={logo.src} className="w-10" />
						<p className={`hidden md:inline ${pacifico.className}`}>Streaker</p>
					</Link>
				</li>
				{!!tokens && (
					<>
						<li>
							<Link href="/streaks">Streaks</Link>
						</li>
					</>
				)}
			</ul>
			{tokens ? (
				<ul className="flex items-center gap-10">
					<li>
						<Link href="/logout">
							Logout <FontAwesomeIcon icon={faSignOut} />
						</Link>
					</li>
				</ul>
			) : (
				<ul className="flex gap-10">
					<li>
						<Link href="/login">
							Login <FontAwesomeIcon icon={faSignIn} />
						</Link>
					</li>
					<li>
						<Link href="/signup">
							Signup <FontAwesomeIcon icon={faUserPlus} />
						</Link>
					</li>
				</ul>
			)}
		</nav>
	);
}
