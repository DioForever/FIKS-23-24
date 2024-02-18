Ahoj fiksáku,

tohle je druhé FIKS CTF (Capture The Flag).
V tomto CTF tě čekají čtyři ukryté tajné vlajky (hesela), každá v podúloze v každé složce.
Podúlohy jsou na sobě nezávislé.
Vlajky jsou vždy ve formátu `fiks{.*?}` (regex) např. `fiks{nejaky_text_12345678}`.

Za každou nalezenou vlajku dostaneš body (za vlajky může být různé množství bodů podle obtížnosti).

CTF je diverzní a zkouší vynalézavost v mnoha směrech.
Je docela pravděpodobné, že ho nevyřešíš celé, ale to nevadí, každý bodík se počítá.

Vlajky se do sfinx odevzdávají v textovém souboru, každá vlajka na samostatném řádku.
Součástí zipu je soubor `checker.py`, kterým si můžeš ověřit, zda jsou tvé vlajky správné.

POZOR: Odevzdání je myšleno jedno na celé kolo.
Vlajky jsou randomizované pro každý vygenerovaný vstup.
Pokud se rozhodneš teď odevzdat dvě vlajky s tím, že zbytek odevzdáš později,
při dalším vygenerování budou vlajky jiné a ty staré ti nebudou uznány.
Proto máš k dispozici checker, kterým si je můžeš před odevzdáním ověřit u sebe.

Hodně štěstí!

PS: Soubory `.seed.txt` a `.hashes.txt` nejsou potřeba na nalezení žádné vlajky, slouží pouze na kontrolu.
