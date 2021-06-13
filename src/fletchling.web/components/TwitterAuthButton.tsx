import { faTwitter } from '@fortawesome/free-brands-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useContext } from 'react';

import GlobalStateContext from '@/components/Context/GlobalStateContext';
import { signOut, twitterSignIn } from '@/lib/firebase';

const TwitterAuthButton = () => {
  const { globalState, setGlobalState } = useContext(GlobalStateContext);

  const onClick = async () => {
    setGlobalState({ ...globalState, isLoading: true });

    if (globalState.currentUser) {
      signOut();
    } else {
      twitterSignIn();
    }
  };

  return (
    <button
      className='flex gap-x-3 py-2 px-3 bg-blue-400 rounded-md'
      onClick={async () => await onClick()}>
      <div>
        <FontAwesomeIcon icon={faTwitter} className='text-white' />
      </div>
      <span>{globalState.currentUser ? 'Log out' : 'Sign in with Twitter'}</span>
    </button>
  );
};

export default TwitterAuthButton;
