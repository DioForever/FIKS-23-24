int __cdecl length_counter(int a1)
{
  int counter; // [esp+0h] [ebp-4h]

  for ( counter = 0; *(counter + a1) && *(counter + a1) != 13; ++counter )
    ;
  return counter;
}