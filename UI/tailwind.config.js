/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    '!**/{bin,obj,node_modules}/**',
    '**/*.{razor,html}',
  ],
  theme: {
    extend: {
      backgroundImage: {
        'main-pattern': "url('/images/background.svg')",
      }
    },
  },
  plugins: [require('@tailwindcss/forms')]
}

