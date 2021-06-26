module.exports = {
  purge: ['./pages/**/*.{js,ts,jsx,tsx}', './components/**/*.{js,ts,jsx,tsx}'],
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {
      colors: {
        'primary-dark': '#1A1A1B',
        'twitter-dark': '#292F33'
      },
      minWidth: {
        80: '20rem'
      }
    }
  },
  variants: {
    extend: {
      borderWidth: ['hover', 'focus']
    }
  },
  plugins: []
};
