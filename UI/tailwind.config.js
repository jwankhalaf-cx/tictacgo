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
      },
      screens: {
        'sm': '400px',
      },
    },
  },
  plugins: [require('@tailwindcss/forms')]
}

