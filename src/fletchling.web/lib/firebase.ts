/* eslint-disable simple-import-sort/imports */
import firebase from 'firebase/app';
import 'firebase/analytics';
import 'firebase/auth';

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

function twitterSignIn() {
  firebaseApp
    .auth()
    .signInWithPopup(twitterProvider)
    .then((result) => console.log(result))
    .catch((error) => console.log(error));
}

function signOut() {
  firebaseApp
    .auth()
    .signOut()
    .then(() => console.log('test'))
    .catch((error) => console.log(error));
}

export { firebaseApp, signOut, twitterProvider, twitterSignIn };
