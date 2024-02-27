import type { Config } from "tailwindcss";
import colors from "tailwindcss/colors";

const config: Config = {
	content: [
		"./pages/**/*.{js,ts,jsx,tsx,mdx}",
		"./components/**/*.{js,ts,jsx,tsx,mdx}",
		"./app/**/*.{js,ts,jsx,tsx,mdx}",
	],
	theme: {
		colors: {
			background: {
				DEFAULT: colors.slate["800"],
				light: colors.slate["700"],
			},
			primary: {
				DEFAULT: colors.emerald["500"],
				dark: colors.emerald["600"],
			},
			secondary: {
				DEFAULT: colors.white,
				dark: colors.gray["200"],
			},
			danger: colors.red["500"],
			white: colors.white,
			black: colors.black,
			gray: colors.gray,
			muted: colors.gray["600"],
		},
	},
	plugins: [],
};
export default config;
