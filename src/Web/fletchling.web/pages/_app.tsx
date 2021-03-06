import 'tailwindcss/tailwind.css';

import type { AppProps } from 'next/app';

import GlobalStateProvider from '@/components/Context/GlobalStateProvider';
import PageLoading from '@/components/PageLoading';

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <GlobalStateProvider>
      <>
        <PageLoading />
        <Component {...pageProps} />
        <style jsx global>{`
          body {
            background-color: #1a1a1b;
          }
        `}</style>
      </>
    </GlobalStateProvider>
  );
}

export default MyApp;
