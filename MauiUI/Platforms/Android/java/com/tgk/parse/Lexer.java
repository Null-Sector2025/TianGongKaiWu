package com.tgk.parse;
public class Lexer {
    private GrammarStore store;
    public Lexer(String grammarRaw) {
        store = new GrammarStore();
        store.loadFromRawTxt(grammarRaw);
    }
    public String translateToLang(String pinyinCode, String lang) {
        String result = pinyinCode;
        for (var entry : store.getMap().entrySet()) {
            String replacement = entry.getValue();
            if (replacement.contains(" ")) replacement = replacement.split(" ")[0];
            result = result.replace(entry.getKey(), replacement);
        }
        return result;
    }
}
