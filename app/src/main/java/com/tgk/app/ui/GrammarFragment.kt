package com.tgk.app.ui

import android.os.Bundle
import android.os.Environment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import com.tgk.app.core.GrammarManager
import com.tgk.app.databinding.FragmentGrammarBinding
import java.io.File

class GrammarFragment : Fragment() {
    private var _binding: FragmentGrammarBinding? = null
    private val binding get() = _binding!!

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        _binding = FragmentGrammarBinding.inflate(inflater, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        showGrammar()
        binding.btnExport.setOnClickListener { exportGrammar() }
        binding.btnImport.setOnClickListener { importGrammar() }
    }

    private fun showGrammar() {
        val text = GrammarManager.getRawText()
        binding.textGrammar.text = text
    }

    private fun exportGrammar() {
        val dir = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DOWNLOADS)
        val file = File(dir, "grammar_map.txt")
        GrammarManager.exportToFile(file.absolutePath)
        Toast.makeText(requireContext(), "已导出到 ${file.absolutePath}", Toast.LENGTH_SHORT).show()
    }

    private fun importGrammar() {
        val dir = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DOWNLOADS)
        val file = File(dir, "grammar_map.txt")
        if (file.exists()) {
            GrammarManager.importFromFile(file.absolutePath)
            showGrammar()
            Toast.makeText(requireContext(), "已导入", Toast.LENGTH_SHORT).show()
        } else {
            Toast.makeText(requireContext(), "文件不存在：${file.absolutePath}", Toast.LENGTH_SHORT).show()
        }
    }

    override fun onDestroyView() { super.onDestroyView(); _binding = null }
}
