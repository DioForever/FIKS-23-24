int __cdecl sub_401198(int a1)
{
  int var2; // [esp+0h] [ebp-8h]
  int i; // [esp+4h] [ebp-4h]

  var2 = 0;
  for ( i = 0; *(i + a1); ++i )
    var2 = 10 * var2 + *(i + a1) - 48;
  return var2;
}