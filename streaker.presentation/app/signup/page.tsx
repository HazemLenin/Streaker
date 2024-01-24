"use client";
import { FormEvent, useRef, useState } from "react";
import useAxios from "../hooks.useAxios";

export default function Page() {
	const axios = useAxios();
	const email = useRef(null);
	const password = useRef(null);
	const confirmPassword = useRef(null);
	const [errors, setErrors] = useState([]);

	function handleSubmit(e: FormEvent) {
		e.preventDefault();
		console.log(email);
		console.log(password);
		console.log(confirmPassword);
	}
	return (
		<div className="flex flex-col gap-20 px-5">
			<form
				className="form flex flex-col gap-10 items-center"
				onSubmit={handleSubmit}
			>
				<h1 className="text-4xl">Signup</h1>

				{errors && (
					<ul>
						{errors.map((error) => (
							<li className="text-danger">{error}</li>
						))}
					</ul>
				)}

				<div className="form-group">
					<label>Email/Username</label>
					<input
						ref={email}
						type="text"
						className="form-control"
						placeholder="Email/Username"
					/>
				</div>
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
				<button type="submit" className="btn btn-primary">
					Signup
				</button>
			</form>
		</div>
	);
}
