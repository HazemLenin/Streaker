"use client";
import {
	faCircleNotch,
	faEye,
	faEyeSlash,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { FormEvent, useRef, useState } from "react";
import useAxios from "../hooks/useAxios";
import { useDispatch } from "react-redux";
import { set_tokens } from "../actions";
import { useRouter } from "next/navigation";

export default function Page() {
	const [showPassword, setShowPassword] = useState(false);
	const email = useRef<HTMLInputElement>(null);
	const password = useRef<HTMLInputElement>(null);
	const axios = useAxios();
	const dispatch = useDispatch();
	const [errors, setErrors] = useState<Array<String>>([]);
	const [loading, setLoading] = useState(false);
	const router = useRouter();

	function togglePassword() {
		setShowPassword(!showPassword);
	}

	function handleSubmit(e: FormEvent) {
		e.preventDefault();
		setErrors([]);
		setLoading(true);
		axios
			.post(`/api/auth/login`, {
				email: email.current?.value,
				password: password.current?.value,
			})
			.then((res) => {
				dispatch(set_tokens(res.data.data));
				router.push("/");
			})
			.catch((err) => {
				if (err.response.status == 400) {
					setErrors([...errors, "incorrect email/username or password."]);
				}
			})
			.finally(() => {
				setLoading(false);
			});
	}

	return (
		<div className="flex justify-center items-center pt-10">
			<form className="form" onSubmit={handleSubmit}>
				<h1 className="text-4xl text-center hidden md:block">Login</h1>
				<div className="flex justify-center gap-10 items-center">
					<div className="flex flex-col gap-5 items-center">
						<h1 className="text-4xl text-center md:hidden">Login</h1>
						{errors.length != 0 && (
							<ul className="text-danger">
								{errors.map((error, index) => (
									<li key={index}>{error}</li>
								))}
							</ul>
						)}
						<div className="form-group">
							<label>Email/Username</label>
							<input
								type="text"
								className="form-control"
								placeholder="Email/Username"
								ref={email}
							/>
						</div>
						<div className="form-group">
							<label>Password</label>
							<div className="relative">
								<input
									type={showPassword ? "text" : "password"}
									className="form-control"
									placeholder="Password"
									ref={password}
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
							{loading ? (
								<FontAwesomeIcon icon={faCircleNotch} spin />
							) : (
								"Login"
							)}
						</button>
					</div>
					<img
						className="hidden md:block"
						src="/svg/Privacy policy-rafiki.svg"
						width={300}
					/>
				</div>
			</form>
		</div>
	);
}
