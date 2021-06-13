/* eslint-disable simple-import-sort/imports */
/* eslint-disable @typescript-eslint/ban-ts-comment */
import firebase from 'firebase/app';
import 'firebase/analytics';
import 'firebase/auth';

import axios from '@/lib/axios';
import User from '@/interfaces/User';

const firebaseConfig = {
  apiKey: process.env.NEXT_PUBLIC_FIREBASE_API_KEY,
  authDomain: process.env.NEXT_PUBLIC_FIREBASE_AUTH_DOMAIN,
  projectId: process.env.NEXT_PUBLIC_FIREBASE_PROJECT_ID,
  storageBucket: process.env.NEXT_PUBLIC_FIREBASE_STORAGE_BUCKET,
  messagingSenderId: process.env.NEXT_PUBLIC_FIREBASE_MESSAGING_SENDER_ID,
  appId: process.env.NEXT_PUBLIC_FIREBASE_APP_ID,
  measurementId: process.env.NEXT_PUBLIC_FIREBASE_MEASUREMENT_ID
};

const firebaseApp = firebase.apps.length ? firebase.app() : firebase.initializeApp(firebaseConfig);
const twitterProvider = new firebase.auth.TwitterAuthProvider();

async function twitterSignIn() {
  console.log('started');
  firebaseApp
    .auth()
    .signInWithPopup(twitterProvider)
    .then((result) => {
      if (result.additionalUserInfo?.isNewUser) {
        const credentials: User = {
          uid: result.user?.uid as string,
          // @ts-ignore
          twitterUserId: result.additionalUserInfo?.profile?.id,
          // @ts-ignore
          accessToken: result.credential?.accessToken,
          // @ts-ignore
          accessTokenSecret: result.credential?.secret
        };

        axios.post('/user', credentials).catch((error) => {
          console.log(error);
          signOut();
        });
      }
    })
    .catch((error) => console.log(error));
}

async function signOut() {
  firebaseApp
    .auth()
    .signOut()
    .catch((error) => console.log(error));
}

export { firebaseApp, signOut, twitterProvider, twitterSignIn };
