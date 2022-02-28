import 'tailwindcss/tailwind.css';

import type { AppProps } from 'next/app';
import { Provider } from 'next-auth/client';

import PageLoading from '@/components/PageLoading';

function MyApp({ Component, pageProps: { session, ...pageProps } }: AppProps) {
  return (
    <Provider session={session}>
      {/* <GlobalStateProvider> */}
      <>
        <PageLoading />
        <Component {...pageProps} />
        <style jsx global>{`
          body {
            background-color: #1a1a1b;
          }
        `}</style>
      </>
      {/* </GlobalStateProvider> */}
    </Provider>
  );
}

export default MyApp;
