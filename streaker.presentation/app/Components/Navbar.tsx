import Link from "next/link";
import logo from "../../public/Streaker Logo.png";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
	faDoorOpen,
	faSignIn,
	faUserPlus,
} from "@fortawesome/free-solid-svg-icons";

export default function Navbar() {
	return (
		<nav className="flex items-center justify-between px-3 mb-10 h-16 border-b border-muted shadow-lg">
			<ul className="flex gap-10">
				<li>
					<Link href="/">
						<img src={logo.src} className="w-10" />
					</Link>
				</li>
			</ul>
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
		</nav>
	);
}
