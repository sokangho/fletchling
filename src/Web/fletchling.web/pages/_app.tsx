import 'tailwindcss/tailwind.css';

import { NextComponentType, NextPageContext } from 'next';
import type { AppProps } from 'next/app';
import { Provider } from 'next-auth/client';
import { QueryClient, QueryClientProvider } from 'react-query';

import AuthGuard from '@/components/Auth/AuthGuard';

type AppAuthProps = AppProps & {
  // eslint-disable-next-line
  Component: NextComponentType<NextPageContext, any, {}> & {
    requireAuth?: boolean;
  };
};

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: false
    }
  }
});

function MyApp({ Component, pageProps }: AppAuthProps) {
  return (
    <Provider session={pageProps.session}>
      <QueryClientProvider client={queryClient}>
        {Component.requireAuth ? (
          <AuthGuard>
            <Component {...pageProps} />
          </AuthGuard>
        ) : (
          <Component {...pageProps} />
        )}
      </QueryClientProvider>
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
