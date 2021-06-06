import 'tailwindcss/tailwind.css';

import type { AppProps } from 'next/app';

import AuthProvider from '@/components/Context/AuthProvider';

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <AuthProvider>
      <Component {...pageProps} />
    </AuthProvider>
  );
}

export default MyApp;
