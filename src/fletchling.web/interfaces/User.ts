interface User {
  id: bigint;
  username: string;
  displayName: string;
  verified: boolean;
  url: string;
  profileImageUrl: string;
}

export default User;
