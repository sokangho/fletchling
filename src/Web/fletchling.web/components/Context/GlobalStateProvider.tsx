import { ReactChild, ReactChildren, useEffect, useMemo, useState } from 'react';

import GlobalStateContext, {
  GlobalState,
  initialGlobalState
} from '@/components/Context/GlobalStateContext';
import TwitterUser from '@/interfaces/TwitterUser';
import { fetcher } from '@/lib/axios';
import { firebaseApp } from '@/lib/firebase';

interface Props {
  children: ReactChild | ReactChildren;
}

const GlobalStateProvider = ({ children }: Props) => {
  const [globalState, setGlobalState] = useState<GlobalState>(initialGlobalState);
  const contextValue = useMemo(
    () => ({ globalState, setGlobalState }),
    [globalState, setGlobalState]
  );

  useEffect(() => {
    // Listen for any auth changes (logged in, logged out)
    const unsubscribe = firebaseApp.auth().onAuthStateChanged((user) => {
      setGlobalState({ ...globalState, isLoading: true });

      if (user) {
        const twitterUserId = user.providerData[0]?.uid;

        user.getIdToken(true).then(async (token) => {
          const twitterUser = await fetcher<TwitterUser>(
            `/twitter/user/get?twitteruserid=${twitterUserId}`,
            token
          );

          setGlobalState({
            ...globalState,
            isLoading: false,
            currentUser: { uid: user.uid, token, twitterUser }
          });
        });
      } else {
        setGlobalState({ ...globalState, isLoading: false, currentUser: null });
      }
    });

    // Stop listening to auth changes when component unmount
    return unsubscribe;
  }, []);

  return <GlobalStateContext.Provider value={contextValue}>{children}</GlobalStateContext.Provider>;
};

export default GlobalStateProvider;
