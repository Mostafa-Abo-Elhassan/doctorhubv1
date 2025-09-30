// vite.config.js أو vite.config.ts
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'

export default defineConfig({
  plugins: [
    react(),        // لازم يفضل موجود عشان JSX و Fast Refresh
    tailwindcss(),  // Plugin الرسمي لتيلويند مع Vite (v4)
  ],
})
