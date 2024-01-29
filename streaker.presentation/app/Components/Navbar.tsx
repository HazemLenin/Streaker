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

export default function Navbar() {
	const tokens = useSelector((state: IRootState) => state.tokens);

	return (
		<nav className="flex items-center justify-between px-3 mb-10 h-16 border-b border-muted shadow-lg">
			<ul className="flex gap-10">
				<li>
					<Link href="/">
						<img src={logo.src} className="w-10" />
					</Link>
				</li>
			</ul>
			{tokens ? (
				<ul className="flex gap-10">
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
