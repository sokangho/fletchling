import { useRouter } from 'next/router';
import { useSession } from 'next-auth/client';
import { ReactNode, useEffect } from 'react';

import PageLoading from '../PageLoading';

const redirectKey = 'sign_in_redirect';

function getRedirect(): string | null {
  return window.sessionStorage.getItem(redirectKey);
}

function setRedirect(redirectUrl: string) {
  window.sessionStorage.setItem(redirectKey, redirectUrl);
}

function clearRedirect() {
  return window.sessionStorage.removeItem(redirectKey);
}

function AuthGuard({ children }: { children: ReactNode }) {
  const [session, loading] = useSession();
  const router = useRouter();
  const isSignedIn = !!session?.twitterUser;

  useEffect(() => {
    if (!loading && !isSignedIn) {
      // Save the url the user is on before redirecting
      setRedirect(router.route);
      // Redirect to sign in page
      router.push('/signin');
    }
  }, [loading, isSignedIn]);

  // Show loading indicator while fetching session
  if (loading) return <PageLoading />;

  // When session is fetched and user is valid, display the components
  if (!loading && isSignedIn) return <>{children}</>;

  return null;
}

export default AuthGuard;
export { clearRedirect, getRedirect };
