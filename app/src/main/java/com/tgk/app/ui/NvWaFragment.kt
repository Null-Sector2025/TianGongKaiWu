package com.tgk.app.ui

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import com.tgk.app.core.PinyinInterpreter
import com.tgk.app.core.SyntaxValidator
import com.tgk.app.databinding.FragmentNvwaBinding

class NvWaFragment : Fragment() {
    private var _binding: FragmentNvwaBinding? = null
    private val binding get() = _binding!!

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        _binding = FragmentNvwaBinding.inflate(inflater, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        binding.btnRun.setOnClickListener { runCode() }
    }

    private fun runCode() {
        val code = binding.inputCode.text.toString()
        if (code.isBlank()) { Toast.makeText(requireContext(), "代码为空", Toast.LENGTH_SHORT).show(); return }
        if (SyntaxValidator.containsNdk(code)) {
            Toast.makeText(requireContext(), "女娲模式禁止 NDK 关键字", Toast.LENGTH_SHORT).show(); return
        }
        binding.outputText.text = ""
        val interpreter = PinyinInterpreter { output ->
            activity?.runOnUiThread { binding.outputText.append("$output\n") }
        }
        interpreter.run(code)
    }

    override fun onDestroyView() { super.onDestroyView(); _binding = null }
}
