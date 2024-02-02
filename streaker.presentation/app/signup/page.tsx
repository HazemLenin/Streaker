"use client";
import { FormEvent, useRef, useState } from "react";
import useAxios from "../hooks/useAxios";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
	faCircleNotch,
	faSpinner,
	faTruckLoading,
} from "@fortawesome/free-solid-svg-icons";
import { toast } from "react-toastify";
import { useRouter } from "next/navigation";
import { useDispatch } from "react-redux";
import { set_tokens } from "../actions";

export default function Page() {
	const axios = useAxios();
	const firstName = useRef<HTMLInputElement>(null);
	const lastName = useRef<HTMLInputElement>(null);
	const email = useRef<HTMLInputElement>(null);
	const username = useRef<HTMLInputElement>(null);
	const password = useRef<HTMLInputElement>(null);
	const confirmPassword = useRef<HTMLInputElement>(null);
	const [errors, setErrors] = useState<Array<String>>([]);
	const [loading, setLoading] = useState(false);
	const [duplicateEmail, setDuplicateEmail] = useState(false);
	const [duplicateUserName, setDuplicateUserName] = useState(false);
	const router = useRouter();
	const dispatch = useDispatch();

	function handleSubmit(e: FormEvent) {
		setLoading(true);
		setErrors([]);
		e.preventDefault();
		if (password.current?.value != confirmPassword.current?.value) {
			setErrors([...errors, "Password don't match"]);
			return;
		}
		axios
			.post("/api/auth/signup", {
				firstName: firstName.current?.value as string,
				lastName: lastName.current?.value as string,
				email: email.current?.value as string,
				userName: username.current?.value as string,
				password: password.current?.value as string,
			})
			.then((res) => {
				setLoading(false);
				dispatch(set_tokens(res.data));
				toast.success("You signed up successfully!", {
					theme: "colored",
				});
				router.push("/");
			})
			.catch((err) => {
				for (let key of Object.keys(err.response.data.errors)) {
					if (key != "DuplicateEmail" && key != "DuplicateUserName")
						setErrors([...errors, err.response.data.errors[key]]);
				}
				// PasswordRequiresDigit
				// PasswordRequiresNonAlphanumeric
				// PasswordRequiresUpper
				// PasswordTooShort
				// DuplicateEmail
				// DuplicateUserName
			})
			.finally(() => {
				setLoading(false);
			});
	}
	return (
		<div className="flex flex-col items-center gap-20 px-5 pt-10">
			<form
				className="form flex flex-col gap-10 items-center md:w-1/3"
				onSubmit={handleSubmit}
			>
				<h1 className="text-4xl">Signup</h1>

				{errors.length != 0 && (
					<ul className="text-danger">
						{errors.map((error, index) => (
							<li key={index}>{error}</li>
						))}
					</ul>
				)}

				<div className="flex gap-5">
					<div className="form-group">
						<label>First Name</label>
						<input
							ref={firstName}
							type="text"
							className="form-control"
							placeholder="First Name"
						/>
					</div>
					<div className="form-group">
						<label>Last Name</label>
						<input
							ref={lastName}
							type="text"
							className="form-control"
							placeholder="Last Name"
						/>
					</div>
				</div>
				<div className="flex gap-5">
					<div className="form-group">
						<label>Email</label>
						<input
							ref={email}
							type="email"
							className="form-control"
							placeholder="Email"
						/>
						{duplicateEmail && (
							<p className="text-danger">Email is already taken.</p>
						)}
					</div>
					<div className="form-group">
						<label>Username</label>
						<input
							ref={username}
							type="text"
							className="form-control"
							placeholder="Username"
						/>
						{duplicateUserName && (
							<p className="text-danger">Username is already taken.</p>
						)}
					</div>
				</div>
				<div className="flex gap-5">
					<div className="form-group">
						<label>Password</label>
						<input
							ref={password}
							type="password"
							className="form-control"
							placeholder="Password"
						/>
					</div>
					<div className="form-group">
						<label>Confirm Password</label>
						<input
							ref={confirmPassword}
							type="password"
							className="form-control"
							placeholder="Confirm Password"
						/>
					</div>
				</div>
				<button type="submit" className="btn btn-primary">
					{loading ? <FontAwesomeIcon icon={faCircleNotch} spin /> : "Signup"}
				</button>
			</form>
		</div>
	);
}
