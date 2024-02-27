export default function truncateText(text: string, limit: number): string {
	const words = text.split(" ");
	if (words.length > limit) {
		return words.slice(0, limit).join(" ") + "...";
	}
	return text;
}
