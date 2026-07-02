package com.tgk.parse;
import java.util.HashMap;
public class GrammarStore {
    private HashMap<String, String> map = new HashMap<>();
    public void loadFromRawTxt(String raw) {
        map.clear();
        for (String line : raw.split("\n")) {
            line = line.trim();
            if (line.isEmpty()) continue;
            String[] kv = line.split("\\|");
            if (kv.length == 2) map.put(kv[0].trim(), kv[1].trim());
        }
    }
    public HashMap<String, String> getMap() { return map; }
}
