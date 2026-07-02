package com.tgk.app.ui

import android.animation.ValueAnimator
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.animation.OvershootInterpolator
import android.widget.Toast
import androidx.core.view.isVisible
import androidx.fragment.app.Fragment
import com.google.android.material.dialog.MaterialAlertDialogBuilder
import com.tgk.app.core.*
import com.tgk.app.databinding.FragmentPanguBinding

class PanGuFragment : Fragment() {

    private var _binding: FragmentPanguBinding? = null
    private val binding get() = _binding!!

    private var isPanGuMode = true
    private var currentFilePath: String? = null

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        _binding = FragmentPanguBinding.inflate(inflater, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        GrammarManager.init(requireContext())
        ProjectManager.init(requireContext())

        updateModeUI()

        binding.btnJava.setOnClickListener { animateView(it); translate("java") }
        binding.btnCSharp.setOnClickListener { animateView(it); translate("csharp") }
        binding.btnPython.setOnClickListener { animateView(it); translate("python") }
        binding.btnJs.setOnClickListener { animateView(it); translate("javascript") }
        binding.btnExecute.setOnClickListener { animateView(it); executeCode() }

        binding.chipMode.setOnCheckedChangeListener { _, isChecked ->
            isPanGuMode = isChecked
            updateModeUI()
        }

        binding.btnOpen.setOnClickListener { openProject() }
        binding.btnSave.setOnClickListener { saveProject() }
    }

    private fun updateModeUI() {
        if (isPanGuMode) {
            binding.chipMode.text = "🐉 盘古模式"
            binding.btnExecute.isVisible = true
            binding.btnJava.isEnabled = true; binding.btnCSharp.isEnabled = true
            binding.btnPython.isEnabled = true; binding.btnJs.isEnabled = true
        } else {
            binding.chipMode.text = "🌸 女娲模式"
            binding.btnExecute.isVisible = true
            binding.btnJava.isEnabled = false; binding.btnCSharp.isEnabled = false
            binding.btnPython.isEnabled = false; binding.btnJs.isEnabled = false
        }
    }

    private fun translate(lang: String) {
        val code = binding.inputCode.text.toString()
        if (code.isBlank()) { showError("代码为空"); return }
        val (valid, msg) = SyntaxValidator.validate(code)
        if (!valid) { showError(msg); return }
        if (!isPanGuMode && SyntaxValidator.containsNdk(code)) {
            showError("女娲模式禁止使用 NDK 关键字"); return
        }
        val result = PinyinTranslator.translate(code, lang)
        binding.outputCode.setText(result)
    }

    private fun executeCode() {
        val code = binding.inputCode.text.toString()
        if (code.isBlank()) { showError("代码为空"); return }
        if (!isPanGuMode && SyntaxValidator.containsNdk(code)) {
            showError("女娲模式禁止使用 NDK 关键字"); return
        }
        binding.outputCode.setText("")
        val interpreter = PinyinInterpreter { output ->
            activity?.runOnUiThread { binding.outputCode.append("$output\n") }
        }
        interpreter.run(code)
    }

    private fun openProject() {
        val files = ProjectManager.getProjects()
        if (files.isEmpty()) {
            Toast.makeText(requireContext(), "暂无项目", Toast.LENGTH_SHORT).show(); return
        }
        val names = files.map { it.name }.toTypedArray()
        MaterialAlertDialogBuilder(requireContext())
            .setTitle("选择项目")
            .setItems(names) { _, which ->
                val file = files[which]
                currentFilePath = file.absolutePath
                binding.inputCode.setText(ProjectManager.readProject(file))
            }.show()
    }

    private fun saveProject() {
        val name = binding.inputName.text?.toString()?.trim()
        if (name.isNullOrEmpty()) { Toast.makeText(requireContext(), "输入名称", Toast.LENGTH_SHORT).show(); return }
        val file = ProjectManager.createProject("$name.pinyin")
        ProjectManager.saveProject(file, binding.inputCode.text.toString())
        currentFilePath = file.absolutePath
        Toast.makeText(requireContext(), "已保存：${file.name}", Toast.LENGTH_SHORT).show()
    }

    private fun showError(msg: String) { binding.outputCode.setText("❌ $msg") }
    private fun animateView(view: View) {
        ValueAnimator.ofFloat(1f, 0.9f, 1f).apply {
            duration = 200; interpolator = OvershootInterpolator()
            addUpdateListener { view.scaleX = it.animatedValue as Float; view.scaleY = it.animatedValue as Float }
            start()
        }
    }

    override fun onDestroyView() { super.onDestroyView(); _binding = null }
}
