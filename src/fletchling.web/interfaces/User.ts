interface User {
  id: bigint;
  username: string;
  displayName: string;
  verified: boolean;
  profileUrl: string;
  profileImageUrl: string;
}

export default User;
