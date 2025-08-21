import { writable } from "svelte/store";
import { browser } from "$app/environment";

// Theme type
export type Theme = "light" | "dark";

// Get initial theme from localStorage or system preference
function getInitialTheme(): Theme {
	if (!browser) return "light";

	// Check localStorage first
	const stored = localStorage.getItem("theme") as Theme;
	if (stored) return stored;

	// Check system preference
	if (window.matchMedia("(prefers-color-scheme: dark)").matches) {
		return "dark";
	}

	return "light";
}

// Create the theme store
export const theme = writable<Theme>(getInitialTheme());

// Theme functions
export const themeActions = {
	toggle: () => {
		theme.update((current) => {
			const newTheme = current === "light" ? "dark" : "light";
			if (browser) {
				localStorage.setItem("theme", newTheme);
				document.documentElement.classList.toggle("dark", newTheme === "dark");
			}
			return newTheme;
		});
	},

	set: (newTheme: Theme) => {
		theme.set(newTheme);
		if (browser) {
			localStorage.setItem("theme", newTheme);
			document.documentElement.classList.toggle("dark", newTheme === "dark");
		}
	},

	init: () => {
		if (browser) {
			const currentTheme = getInitialTheme();
			theme.set(currentTheme);
			document.documentElement.classList.toggle(
				"dark",
				currentTheme === "dark",
			);

			// Listen for system theme changes
			const mediaQuery = window.matchMedia("(prefers-color-scheme: dark)");
			mediaQuery.addEventListener("change", (e) => {
				if (!localStorage.getItem("theme")) {
					const systemTheme = e.matches ? "dark" : "light";
					themeActions.set(systemTheme);
				}
			});
		}
	},
};
