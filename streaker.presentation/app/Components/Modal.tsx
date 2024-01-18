import { MouseEventHandler, PropsWithChildren, ReactNode, useRef } from "react";

export default function Modal({
	children,
	close,
	large,
}: {
	children: ReactNode;
	close: Function;
	large?: boolean;
}) {
	const modal = useRef<HTMLDivElement>(null);

	const handleBackdropClick: MouseEventHandler<HTMLDivElement> = (event) => {
		if (event.target == modal.current) close();
	};

	return (
		<div
			className="fixed h-screen w-screen bg-black/50 flex justify-center items-center"
			ref={modal}
			onClick={handleBackdropClick}
		>
			<div
				className={`bg-background border border-muted rounded-2xl p-6 ${
					large ? "w-2/3" : "w-1/3"
				}`}
			>
				{children}
			</div>
		</div>
	);
}
