import { sveltekit } from "@sveltejs/kit/vite";
import { defineConfig } from "vite";

export default defineConfig({
	plugins: [sveltekit()],
	server: {
		port: 5173,
		host: true,
		fs: {
			allow: [".."],
		},
	},
	preview: {
		port: 4173,
		host: true,
	},
	build: {
		target: "esnext",
		minify: "terser",
		rollupOptions: {
			output: {
				manualChunks: {
					vendor: ["svelte", "@sveltejs/kit"],
					lucide: ["lucide-svelte"],
				},
			},
		},
	},
	optimizeDeps: {
		include: ["lucide-svelte"],
	},
});
