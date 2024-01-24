"use client";
import { faEye, faEyeSlash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useState } from "react";

export default function Page() {
	const [showPassword, setShowPassword] = useState(false);

	function togglePassword() {
		setShowPassword(!showPassword);
	}

	return (
		<div className="flex flex-col gap-20 px-5">
			<form className="form flex flex-col gap-10 items-center">
				<h1 className="text-4xl">Login</h1>
				<div className="form-group">
					<label>Email/Username</label>
					<input
						type="text"
						className="form-control"
						placeholder="Email/Username"
					/>
				</div>
				<div className="form-group">
					<label>Password</label>
					<div className="relative">
						<input
							type={showPassword ? "text" : "password"}
							className="form-control"
							placeholder="Password"
						/>
						<button
							type="button"
							className="absolute inset-y-0 end-4 flex items-center"
							onClick={() => togglePassword()}
						>
							{showPassword ? (
								<FontAwesomeIcon icon={faEyeSlash} />
							) : (
								<FontAwesomeIcon icon={faEye} />
							)}
						</button>
					</div>
				</div>
				<button type="submit" className="btn btn-primary">
					Login
				</button>
			</form>
		</div>
	);
}
