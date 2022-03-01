import 'tailwindcss/tailwind.css';

import { NextComponentType, NextPageContext } from 'next';
import type { AppProps } from 'next/app';
import { Provider } from 'next-auth/client';

import AuthGuard from '@/components/Auth/AuthGuard';

type AppAuthProps = AppProps & {
  // eslint-disable-next-line
  Component: NextComponentType<NextPageContext, any, {}> & {
    requireAuth?: boolean;
  };
};

function MyApp({ Component, pageProps }: AppAuthProps) {
  return (
    <Provider session={pageProps.session}>
      {Component.requireAuth ? (
        <AuthGuard>
          <Component {...pageProps} />
        </AuthGuard>
      ) : (
        <Component {...pageProps} />
      )}
    </Provider>
  );
}

export default MyApp;

{
  /* <style jsx global>{`
body {
  background-color: #1a1a1b;
}
`}</style> */
}
