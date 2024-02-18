BOOL __cdecl readUserInput(LPVOID lpBuffer)
{
  HANDLE hFile; // [esp+0h] [ebp-8h]
  DWORD NumberOfBytesRead; // [esp+4h] [ebp-4h] BYREF

  NumberOfBytesRead = 0;
  hFile = GetStdHandle(0xFFFFFFF6);
  return ReadFile(hFile, lpBuffer, 0xFFu, &NumberOfBytesRead, 0);
}