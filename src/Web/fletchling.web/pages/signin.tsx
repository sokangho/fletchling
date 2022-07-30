import { useRouter } from 'next/router';
import { useSession } from 'next-auth/client';

import { clearRedirect, getRedirect } from '@/components/Auth/AuthGuard';
import TwitterSignInButton from '@/components/Buttons/TwitterSignInButton';
import PageLoading from '@/components/PageLoading';

const SignInPage = () => {
  const [session, loading] = useSession();
  const isSignedIn = !!session?.twitterUser;
  const router = useRouter();

  if (!loading && isSignedIn) {
    const redirect = getRedirect();

    if (redirect) {
      // Redirect user to the last page they were on
      router.push(redirect);
      clearRedirect();
    } else {
      // If no redirect url found, redirect to home page
      router.push('/');
    }
  }

  // If session has been fetched and user is not signed in, display sign in button
  if (!loading && !isSignedIn) {
    return (
      <div className='flex h-screen justify-center items-center'>
        <TwitterSignInButton />
      </div>
    );
  }

  // Otherwise display loading screen
  return <PageLoading />;
};

export default SignInPage;