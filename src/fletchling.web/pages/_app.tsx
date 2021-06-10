import 'tailwindcss/tailwind.css';

import type { AppProps } from 'next/app';

import AuthProvider from '@/components/Context/AuthProvider';
import PageLoading from '@/components/PageLoading';

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <AuthProvider>
      <>
        <PageLoading />
        <Component {...pageProps} />
      </>
    </AuthProvider>
  );
}

export default MyApp;
