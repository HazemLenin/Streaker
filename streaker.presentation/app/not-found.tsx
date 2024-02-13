import Link from "next/link";

export default function NotFound() {
	return (
		<div className="flex flex-col gap-5 justify-center items-center px-2 md:px-20 pt-24">
			<img src="/svg/Exploring-pana.svg" className="w-full md:w-1/3" />
			<h1 className="text-4xl font-bold">Got Lost?!</h1>
			<p>
				Return{" "}
				<Link href="/" className="text-primary underline">
					home
				</Link>{" "}
				and find your path!
			</p>
		</div>
	);
}
