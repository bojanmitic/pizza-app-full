const isAuthenticated = (): boolean => {
  const expiresAt = localStorage.getItem('expiresAt');
  const isExpired = expiresAt ? new Date().toISOString() > expiresAt : true;
  const isAuthed = !!localStorage.getItem('token') && !isExpired;

  return isAuthed;
};

export default isAuthenticated;
